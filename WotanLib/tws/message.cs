using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public abstract class messageType {}
    public class tickPrice : messageType {}
    public class tickSize : messageType {}
    public class orderStatus : messageType {}
    public class error : messageType {}
    public class openOrder : messageType { }
    public class accountValue : messageType { }
    public class portfolioValue : messageType { }
    public class accountUpdateTime : messageType { }
    public class nextValidId : messageType { }
    public class contractData : messageType { }
    public class executionData : messageType { }
    public class marketDepth : messageType { }
    public class marketDepthL2 : messageType { }
    public class newsBulletins : messageType { }
    public class managedAccounts : messageType { }
    public class receiveFA : messageType { }
    public class historicalData : messageType { }
    public class bondContractData : messageType { }
    public class scannerParameters : messageType { }
    public class scannerData : messageType { }
    public class tickOptionComputation : messageType { }
    public class tickGeneric : messageType { }
    public class tickstring : messageType { }
    public class tickEFP : messageType { }
    public class currentTime : messageType { }
    public class realTimeBars : messageType { }
    public class fundamentalData : messageType { }
    public class contractDataEnd : messageType { }
    public class openOrderEnd : messageType { }
    public class accountDownloadEnd : messageType { }
    public class executionDataEnd : messageType { }
    public class deltaNeutralValidation : messageType { }
    public class tickSnapshotEnd : messageType { }
    public class marketDataType : messageType { }
    public class commissionsReport : messageType { }
    public class position : messageType { }
    public class positionEnd : messageType { }
    public class accountSummary : messageType { }
    public class accountSummaryEnd : messageType { }
    public class positionMulti : messageType { }
    public class positionMultiEnd : messageType { }
    public class accountUpdateMulti : messageType { }
    public class accountUpdateMultiEnd : messageType { }
    public class securityDefinitionOptionParameter : messageType { }
    public class securityDefinitionOptionParameterEnd : messageType { }
    public class connectionStatus : messageType { }
    public class historicalDataEnd : messageType { }
    public class scannerDataEnd : messageType { }

    public abstract class message<T> where T : messageType
    {
        protected T type_;
        public T type
        {
            get { return type_; }
            set { type_ = value; }
        }
    }
}
