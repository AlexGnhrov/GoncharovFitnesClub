using GoncharovFitnesClub.PnWFolder.WindoFolder.AdminWin.UserData;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.ClientWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Subscription;
using System.Windows.Controls;

namespace GoncharovFitnesClub.ClassFolder
{
    class VariableClass
    {


        //Окно администратора

        public static EditStaffWindow editStaffWindow;

        public static int StaffID;
        public static int UserID;


        public static bool newUserDataCreated;
        public static bool AddStaffWinisUsing;

        // Окно сотрудника        
        public static AddSubscriptionWindow addSubscriptionWindow;

        public static EditCoachWindow editCoachWindow;
        public static EditClientWindow editClienthWindow;
        public static EditSubscriptionWindow editSubscriptionWindow;


        public static DataGrid ListSubscriptionDG;
        public static TabItem SubscriptionTI;
        public static TextBox SearchTB;
        public static Button AddBT;
        public static Label CountUsersLB;

        public static int ClientID;
        public static int CoachID;
        public static int SubscriptionID;

        public static bool CoachWinisUsing;
        public static bool SubscriptionWinisUsing;
        public static bool ClientWinisUsing;

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
