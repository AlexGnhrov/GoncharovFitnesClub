using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.ClientWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Subscription;
using System.Windows.Controls;

namespace GoncharovFitnesClub.ClassFolder
{
    class VariableClass
    {
        public static AddSubscriptionWindow addSubscriptionWindow;

        public static EditCoachWindow editCoachWindow;
        public static EditClientWindow editClienthWindow;
        public static EditSubscriptionWindow editSubscriptionWindow;

        //Окно администратора
        public static int UserID;

        public static bool AddUserWinisUsing;
        public static int CountEditWindowUser = 0;

        public static int[] SelectedUserID = new int[3];

        // Окно сотрудника
        public static int ClientID;
        public static int CoachID;
        public static int SubscriptionID;

        public static bool CoachWinisUsing;
        public static bool SubscriptionWinisUsing;
        public static bool ClientWinisUsing;



        public static DataGrid ListSubscriptionDG;
        public static TabItem SubscriptionTI;
        public static TextBox SearchTB;
        public static Button AddBT;
        public static Label CountUsersLB;



        //Доп. окна
        public static int SpecialityID;
        public static int StatusID;
        public static int DateVisitID;
        public static int TimeVisitID;

        public static bool newSpecialityCreated;
        public static bool newStatusCreated;
        public static bool newVisitDayCreated;
        public static bool newVisitTimeCreated;

     



    }
}
