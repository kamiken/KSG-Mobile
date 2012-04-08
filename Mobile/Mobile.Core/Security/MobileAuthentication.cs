using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mobile.Core.Web;
using System.Web.Security;
using Mobile.Object;
using Mobile.Business;


namespace Mobile.Core.Security
{
    public class MobileAuthentication
    {
        public MobileAuthentication()
        {
            //UserOnline = new List<User>();
        }

        public static int UserId { get { return User.UserId;} }

        public static User User
        {
            get{
                return (User)ContextManager.CurrentSession[ConstantManager.SS_AUTHENTICATION];
            }
            private set { ContextManager.CurrentSession[ConstantManager.SS_AUTHENTICATION] = value; }
        }

        public static bool IsAuthenticated {
            get { return User != null; }
        }

        public static bool SetAuthentication(User user)
        {
            try
            {
                // Add User to application
                var lstUser = UserOnline.Where(p => p.UserId == user.UserId);
                if (lstUser == null || lstUser.Count() == 0)
                {
                    //UserOnline.Add(user);
                    var u = UserOnline;
                    u.Add(user);
                    UserOnline = u;
                }
                else
                {
                    foreach (var item in lstUser)
                    {
                        UserOnline.Remove(item);
                    }
                }
                // Add User to Session
                User = user;
                //FormsAuthentication.SetAuthCookie(user.UserId.ToString(),);
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool Logout(string email = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var objUsers = UserOnline.Where(p => p.Email == email);
                    // Delete user in application
                    foreach (var item in objUsers)
                    {
                        UserOnline.Remove(item);
                    }
                }
                else
                {
                    var objUsers = UserOnline.Where(p => p.UserId == User.UserId);
                    // Delete user in application
                    foreach (var item in objUsers)
                    {
                        UserOnline.Remove(item);
                    }
                }


                // Delete user in Session
                User = null;

                // Delete user in Cookie
                FormsAuthentication.SignOut();
                return true;
            }
            catch {
                return false;
            }
        }

        public static List<User> UserOnline
        {
            get
            {
                var userOnlineList = (List<User>)ContextManager.CurrentApplication[ConstantManager.APP_AUTHENTICATION];
                return userOnlineList != null ? userOnlineList : new List<User>();
            }
            private set {                
                ContextManager.CurrentApplication[ConstantManager.APP_AUTHENTICATION] = value;
            }
        }
                
        public static void Login(string email, string password , out LoginStatus status)
        {
            BLLUser bllUser = new BLLUser();
            var objUser = bllUser.GetUser(email,Encrypt.MD5Encrypt.Encrypt(password,true));
            if (objUser == null)
            {
                status = LoginStatus.LoginFail;
            }
            else if (objUser.IsDeleted)
            {
                status = LoginStatus.UserDeleted;
            }
            else if (objUser.IsLocked)
            {
                status = LoginStatus.UserLocked;
            }
            else if (objUser.IsRequirePwdChange)
            {
                status = LoginStatus.IsRequirePwdChange;
            }                 
            else
                status = LoginStatus.LoginSuccess;
            if (status == LoginStatus.LoginSuccess)
            {
                var obj = new  Mobile.Core.Security.User(){
                    UserId = objUser.UserID,
                    CompanyId = objUser.CompanyID,
                    Email = objUser.Email,
                    FullName = objUser.FullName,                    
                };
                SetAuthentication(obj);
            }
        }

        
    }


}
