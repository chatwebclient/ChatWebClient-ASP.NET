using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatWebClient.Models;
using PusherServer;

namespace ChatWebClient.Controllers
{
    public class AuthController : Controller
    {
    
    	
	private Pusher pusher;

	//class constructor
	public AuthController() 
	{

	    var options = new PusherOptions();
	    options.Cluster = "ap1";

	    pusher = new Pusher(
           "751032",
           "7eb4710cfcfb2b4daca8",
           "9aacd888470a3d9d45af",
	       options
	   );
	}
		[HttpPost]
		public ActionResult Login()
		{
            
			string user_name = Request.Form["username"];

			if (user_name.Trim() == "") {
				return Redirect("/");
			}


            using (var db = new Models.ChatContext()) {

                User user = db.Users.FirstOrDefault(u => u.name == user_name);

                if (user == null) {
                    user = new User { name = user_name };

                    db.Users.Add(user);
                    db.SaveChanges();
                }

                Session["user"] = user;
            }

			return Redirect("/chat");
		}

        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (Models.User)Session["user"];

            if ( channel_name.IndexOf("presence") >= 0 ) {

				var channelData = new PresenceChannelData()
				{
					user_id = currentUser.id.ToString(),
					user_info = new
					{
						id = currentUser.id,
						name = currentUser.name
					},
				};

				var presenceAuth = pusher.Authenticate(channel_name, socket_id, channelData);

				return Json(presenceAuth);

            }

	    if (channel_name.IndexOf(currentUser.id.ToString()) == -1)
	    {
		return Json(new { status = "error", message = "User cannot join channel" });
	    }

	    var auth = pusher.Authenticate(channel_name, socket_id);

	    return Json(auth);


        }
    }
}
