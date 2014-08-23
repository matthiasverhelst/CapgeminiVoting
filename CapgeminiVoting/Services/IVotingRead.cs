using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CapgeminiVoting.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVotingRead" in both code and config file together.
    [ServiceContract]
    public interface IVotingRead
    {
        [OperationContract]
        void DoWork();
    }
}
