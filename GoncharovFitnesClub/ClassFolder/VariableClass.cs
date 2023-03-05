using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoncharovFitnesClub.ClassFolder
{
    class VariableClass
    {

        public static int UserID;

        public static int ClientID;
        public static int CoachID;
        public static int SubscriptionID;

        public static int SpecialityID;
        public static int StatusID;
        public static int DateVisitID;
        public static int TimeVisitID;

        public static bool newSpecialityCreated;
        public static bool newStatusCreated;
        public static bool newVisitDayCreated;
        public static bool newVisitTimeCreated;



        public static bool AddUserWinisUsing;
        public static int CountEditWindowUser = 0;

        public static bool CoachWinisUsing;
        public static bool SubscriptionWinisUsing;
        public static bool ClientWinisUsing;



        public static int[] SelectedUserID = new int[3];
    }
}
