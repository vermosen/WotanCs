using System;
using System.Collections.Generic;
using IBApi;

namespace Wotan
{
    // intermediary class for any EWrapper implementation
    public abstract class eWrapperImpl : EWrapper
    {
        public virtual void accountDownloadEnd(string account)
        {
            throw new NotImplementedException();
        }
        public virtual void accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException();
        }
        public virtual void accountSummaryEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public virtual void accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException();
        }
        public virtual void accountUpdateMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        public virtual void bondContractDetails(int reqId, ContractDetails contract)
        {
            throw new NotImplementedException();
        }
        public virtual void commissionReport(CommissionReport commissionReport)
        {
            throw new NotImplementedException();
        }
        public virtual void connectAck()
        {
            throw new NotImplementedException();
        }
        public virtual void connectionClosed()
        {
            throw new NotImplementedException();
        }
        public virtual void contractDetails(int reqId, ContractDetails contractDetails)
        {
            throw new NotImplementedException();
        }
        public virtual void contractDetailsEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public virtual void currentTime(long time)
        {
            throw new NotImplementedException();
        }
        public virtual void deltaNeutralValidation(int reqId, UnderComp underComp)
        {
            throw new NotImplementedException();
        }
        public virtual void displayGroupList(int reqId, string groups)
        {
            throw new NotImplementedException();
        }
        public virtual void displayGroupUpdated(int reqId, string contractInfo)
        {
            throw new NotImplementedException();
        }
        public virtual void error(string str)
        {
            throw new NotImplementedException();
        }
        public virtual void error(Exception e)
        {
            throw new NotImplementedException();
        }
        public virtual void error(int id, int errorCode, string errorMsg)
        {
            throw new NotImplementedException();
        }
        public virtual void execDetails(int reqId, Contract contract, Execution execution)
        {
            throw new NotImplementedException();
        }
        public virtual void execDetailsEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public virtual void fundamentalData(int reqId, string data)
        {
            throw new NotImplementedException();
        }
        public virtual void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
        {
            throw new NotImplementedException();
        }
        public virtual void historicalDataEnd(int reqId, string start, string end)
        {
            throw new NotImplementedException();
        }
        public virtual void managedAccounts(string accountsList)
        {
            throw new NotImplementedException();
        }
        public virtual void marketDataType(int reqId, int marketDataType)
        {
            throw new NotImplementedException();
        }
        public virtual void nextValidId(int orderId)
        {
            throw new NotImplementedException();
        }
        public virtual void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            throw new NotImplementedException();
        }
        public virtual void openOrderEnd()
        {
            throw new NotImplementedException();
        }
        public virtual void orderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld)
        {
            throw new NotImplementedException();
        }
        public virtual void position(string account, Contract contract, double pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        public virtual void positionEnd()
        {
            throw new NotImplementedException();
        }
        public virtual void positionMulti(int requestId, string account, string modelCode, Contract contract, double pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        public virtual void positionMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        public virtual void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
        {
            throw new NotImplementedException();
        }
        public virtual void receiveFA(int faDataType, string faXmlData)
        {
            throw new NotImplementedException();
        }
        public virtual void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException();
        }
        public virtual void scannerDataEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public virtual void scannerParameters(string xml)
        {
            throw new NotImplementedException();
        }
        public virtual void securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            throw new NotImplementedException();
        }
        public virtual void securityDefinitionOptionParameterEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public virtual void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            throw new NotImplementedException();
        }
        public virtual void tickGeneric(int tickerId, int field, double value)
        {
            throw new NotImplementedException();
        }
        public virtual void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            throw new NotImplementedException();
        }
        public virtual void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            throw new NotImplementedException();
        }
        public virtual void tickSize(int tickerId, int field, int size)
        {
            throw new NotImplementedException();
        }
        public virtual void tickSnapshotEnd(int tickerId)
        {
            throw new NotImplementedException();
        }
        public virtual void tickString(int tickerId, int field, string value)
        {
            throw new NotImplementedException();
        }
        public virtual void updateAccountTime(string timestamp)
        {
            throw new NotImplementedException();
        }
        public virtual void updateAccountValue(string key, string value, string currency, string accountName)
        {
            throw new NotImplementedException();
        }
        public virtual void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            throw new NotImplementedException();
        }
        public virtual void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
        {
            throw new NotImplementedException();
        }
        public virtual void updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            throw new NotImplementedException();
        }
        public virtual void updatePortfolio(Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealisedPNL, double realisedPNL, string accountName)
        {
            throw new NotImplementedException();
        }
        public void verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        public virtual void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            throw new NotImplementedException();
        }
        public virtual void verifyCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        public virtual void verifyMessageAPI(string apiData)
        {
            throw new NotImplementedException();
        }
    }
}
