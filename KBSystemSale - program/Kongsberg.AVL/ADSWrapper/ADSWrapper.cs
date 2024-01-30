using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwinCAT.Ads;
using System.Diagnostics;
using System.Threading;

namespace ADSWrapper
{
    public class ADSWrapper
    {

        public ADSWrapper()
        {
            client = new TcAdsClient();
            client.Connect(801);
        }

        Dictionary<string, ADSHandle> handles = new Dictionary<string, ADSHandle>();
        private TcAdsClient client;

        public void AddHandle(string p, int length = 0)
        {
            var type = typeof(short);
            switch (p[1])
            {
                case 's':
                    type = typeof(string);
                    break;
                case 'n':
                    type = typeof(short);
                    break;
                case 'b':
                    type = typeof(bool);
                    break;
            }
            AddHandle(p, type,length);
        }
        public void AddHandle(string p,Type type,int length=0)
        {
            try
            {
                handles.Add(p, new ADSHandle
                {
                    Type = type,
                    Length = length,
                    Handle = client.CreateVariableHandle(p)
                });
            }
            catch (Exception e)
            {
                //EventLog.WriteEntry("ADSWrapper.ADSWrapper", String.Format("Could not create handle for variable {0}\n{1}",p,e.Message), EventLogEntryType.Error);
            }
        }

        public object Read(string p)
        {
            try
            {
                var handle = handles[p];
                object res = null;
                if (handle.Length == 0)
                {
                    res= client.ReadAny(handle.Handle, handle.Type);
                }
                else
                {
                    res = client.ReadAny(handle.Handle, handle.Type, new int[] { handle.Length });
                }
                //EventLog.WriteEntry(res.ToString(), res.GetType().ToString(), EventLogEntryType.Warning);
                return res;
            }
            catch (Exception e)
            {
                //EventLog.WriteEntry("ADSWrapper.ADSWrapper", String.Format("Could not read from handle for variable {0} {2} {3}\n{1}", p, e.Message, handles[p].Type, handles[p].Length), EventLogEntryType.Error);
            }
            return null;
        }

        public void Write(string p, object p_2)
        {
            try
            {
                if (p_2 is string)
                {
                    client.WriteAny(handles[p].Handle, p_2, new int[] { ((string)p_2).Length });
                }
                else 
                {
                    client.WriteAny(handles[p].Handle, p_2);
                }
            }
            catch (Exception e)
            {
                //EventLog.WriteEntry("ADSWrapper.ADSWrapper", String.Format("Could not write to handle for variable {0}\n{1}", p, e.Message), EventLogEntryType.Error);
            }
        }
        public void AddAction(string p, Action<object> p_2)
        {
            handles[p].Action = p_2;
        }

        public void Close()
        {
            StopListening = true;
        }

        public void StartListening()
        {
            new Thread(new ThreadStart(() =>
            {
                while (!StopListening)
                {
                    foreach (var item in handles.Where(h => h.Value.Action != null))
                    {
                        item.Value.Action(Read(item.Key));
                    }
                    Thread.Sleep(8);
                }
            })).Start();
        }

        public bool StopListening { get; set; }
    }
}
