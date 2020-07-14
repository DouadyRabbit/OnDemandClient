using System;
using System.IO;
using System.Runtime.InteropServices;


//- Ref https://www.ibm.com/support/knowledgecenter/SSB2EG_7.4.0/com.ibm.ondemand.winclient.doc/dodwc249.htm

namespace OnDemandClient
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("OnDemandClient.OnDemandClient")]

    public class OnDemandClient : ArsOLEWrapper
    {
        //-----------------Search Operatos constants------------------
        const short ARS_OLE_OPR_EQUAL = 1;
        const short ARS_OLE_OPR_NOT_EQUAL = 2;
        const short ARS_OLE_OPR_LESS_THAN = 3;

        const short ARS_OLE_OPR_LESS_THAN_OR_EQUAL = 4;
        const short ARS_OLE_OPR_GREATER_THAN = 5;
        const short ARS_OLE_OPR_GREATER_THAN_OR_EQUAL = 6;

        const short ARS_OLE_OPR_BETWEEN = 7;
        const short ARS_OLE_OPR_NOT_BETWEEN = 8;
        const short ARS_OLE_OPR_IN = 9;

        const short ARS_OLE_OPR_NOT_IN = 10;
        const short ARS_OLE_OPR_LIKE = 11;
        const short ARS_OLE_OPR_NOT_LIKE = 12;

        //-----------------ARSOLE Find Types constants ------------------------
        const short ARS_OLE_FIND_FIRST = 1;
        const short ARS_OLE_FIND_PREV = 2;
        const short ARS_OLE_FIND_NEXT = 3;

        //----------------- Parameters  ------------------------

        public string server = "SERVER-NAME-HERE";
        public string userName = "USER-NAME-HERE";
        public string password = "USER-PASSWORD-HERE";

        public string productID = "CJSR";
        public string searchField = "Posting Date";
        public short searchOpr = ARS_OLE_OPR_BETWEEN; // 7 means Between

        public DateTime courtCalendarDate = DateTime.Today;
        public string outPath = Path.GetTempPath();
        public string calendarName = "";

        public bool useNumberDate = false;
        public int daysToSearchBack = 3;

        //--------------------------------
        ArsOLEWrapper oOnD = null;

        //--------------Error feedback---------------------------------
        public int errorCode = 0;
        public string errorMessage = "";

        public OnDemandClient()
        {

            try
            {
                this.oOnD = new ArsOLEWrapper();
            }
            catch (Exception ex)
            {
                this.errorMessage = "Failed initializing OnDemand object" + ex.Message;
                this.errorCode = -1;
            }
        }

        public int ExecOD()
        {
           
            int error_Success = 0;
            try
            {
                error_Success =  oOnD.Logon(server, userName, password);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                errorCode = -1;
                throw;
            }
            
    
            return errorCode;
        }

    }

    //Since the AxInterop.ArsOle.dll file is not generated  I tried using the Interop.ArsOle.dll for the wrapper, but it crashes, it needs both
    public class ArsOLEWrapper : ARSOLELib.ArsOleClass //AxARSOLELib.AxArsOle 
    {
        public ArsOLEWrapper()
        {
            //this next line only when using Axinterop.ArsOle.dll
            //this.CreateControl();
        }

    }
}
