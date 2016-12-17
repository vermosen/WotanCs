namespace Wotan
{
    public enum messageType
    {
        tickPrice = 1,
        tickSize = 2,
        orderStatus = 3,
        error = 4,
        openOrder = 5,
        accountValue = 6,
        portfolioValue = 7,
        accountUpdateTime = 8,
        nextValidId = 9,
        contractData = 10,
        executionData = 11,
        marketDepth = 12,
        marketDepthL2 = 13,
        newsBulletins = 14,
        managedAccounts = 15,
        receiveFA = 16,
        historicalData = 17,
        bondContractData = 18,
        scannerParameters = 19,
        scannerData = 20,
        tickOptionComputation = 21,
        tickGeneric = 45,
        tickstring = 46,
        tickEFP = 47,
        currentTime = 49,
        realTimeBars = 50,
        fundamentalData = 51,
        contractDataEnd = 52,
        openOrderEnd = 53,
        accountDownloadEnd = 54,
        executionDataEnd = 55,
        deltaNeutralValidation = 56,
        tickSnapshotEnd = 57,
        marketDataType = 58,
        commissionsReport = 59,
        position = 61,
        positionEnd = 62,
        accountSummary = 63,
        accountSummaryEnd = 64,
        positionMulti = 71,
        positionMultiEnd = 72,
        accountUpdateMulti = 73,
        accountUpdateMultiEnd = 74,
        securityDefinitionOptionParameter = 75,
        securityDefinitionOptionParameterEnd = 76,

        //Given that the TWS is not sending a termination message for the historical bars, we produce one
        historicalDataEnd = 98,
        scannerDataEnd = 99,
        connectionStatus = 100,
        unknown = 0
    }

    public abstract class message
    {
        public message(messageType type) {this.type = type; }
        public messageType type { get; private set; }
    }
}
