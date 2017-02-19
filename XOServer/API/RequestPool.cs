using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOServer.Authentification;

namespace XOServer.API
{
    public class RequestPool
    {
        public RequestPool()
        {
            Item = new List<Request>();
        }

        public List<Request> Item { get; private set; }

        public Request GetByWantedName(string wantedName)
        {
            IEnumerable<Request> req = null;
            req = from r in Item
                  where r.Wanted.Name.Equals(wantedName)
                  select r;

            return req.First();
        }

        public void Add(Request request)
        {
            Item.Add(request);
            SendRequest(request);
        }

        public void SendRequest(Request request)
        {
            StreamWriter sw = new StreamWriter(request.Wanted.User.GetStream());
            sw.WriteLine("invite:" + request.Wanter.Name);
            sw.Flush();
        }

        public void SendResponse(Request request, string response)
        {
            StreamWriter sw = new StreamWriter(request.Wanted.User.GetStream());
            sw.WriteLine("success:" + response);
            sw.Flush();

            sw = new StreamWriter(request.Wanter.User.GetStream());
            sw.WriteLine("success:" + response);
            sw.Flush();
        }
    }
}
