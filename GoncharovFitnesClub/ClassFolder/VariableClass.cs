﻿using System;
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
        public static int VisitDateID;

        public static bool newSpecialityCreated;
        public static bool newStatusCreated;
        public static bool newVisitDayCreated;




        public static bool AddUserWinisUsing;
        public static int CountEditWindowUser = 0;

        public static bool CoachWinisUsing;
        public static int CountCoacEdithWin = 0;

        public static bool SubscriptionWinisUsing;
        public static int CountSubscriptionWindowUser = 0;

        public static bool ClientWinisUsing;
        public static int CountClientWindowUser = 0;



        public static int[] SelectedUserID = new int[3];
    }
}
