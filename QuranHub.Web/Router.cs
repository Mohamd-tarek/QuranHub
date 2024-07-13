namespace QuranHub.Web
{
    public static class Router
    {

        private const string Base = "api/";
        private const string Version = "v1/";
        private const string Root = Base + Version;
        public static class Account
        {
            private const string Prefix = Root + "account/";
            public const string UserInfo = Prefix + "user-info";
            public const string EditUserInfo = Prefix + "edit-user-info";
            public const string AboutInfo = Prefix + "about-info";
            public const string PrivacySetting = Prefix + "privacy-Setting";
            public const string EditAboutInfo = Prefix + "edit-aAbout-info";
            public const string ChangeEmail = Prefix + "change-email";
            public const string ChangeEmailConfirm = Prefix + "change-email-confirm";
            public const string ChangePassword = Prefix + "change-password";
            public const string RecoverPassword = Prefix + "crecover-password";
            public const string RecoverPasswordConfirm = Prefix + "recover-password-confirm";
            public const string DeleteAccount = Prefix + "delete-account";

        }

        public static class Authenticate
        {
            private const string Prefix = Root + "authentication/";
            public const string LoginWithPassword = Prefix + "login-with-password";
            public const string SignUp = Prefix + "signup";
            public const string SignUpConfirm = Prefix + "signup-confirm";
            public const string SignupResend = Prefix + "signup-resend";
        }

        public static class ExternalAuthentication
        {
            private const string Prefix = Root + "external-authentication/";
            public const string ExternalSchemas = Prefix + "external-schemas";
            public const string LoginWithExternalProvider = Prefix + "login-with-external-provider/{provider}";
            public const string LoginWithExternalProviderCallback = Prefix + "login-with-external-provider-callback";
            public const string SignupWithExternalProvider = Prefix + "signup-with-external-provider/{provider}";
            public const string SignupWithExternalProviderCallback = Prefix + "signup-with-external-provider-callback";


        }
        public static class Analysis
        {
            private const string Prefix = Root + "analysis/";
            public const string Topics = Prefix + "topics";
            public const string SimilarAyas = Prefix + "similar-ayas/{id}";
            public const string Uniques = Prefix + "uniques";
        }

        public static class Documentary
        {
            private const string Prefix = Root + "documentary/";
            public const string PlayListsInfo = Prefix + "play-lists-info";
            public const string PlayListInfo = Prefix + "play-list-info/{PlaylistName}";
            public const string VideoInfoForPlayList = Prefix + "video-info-for-play-list/{playListName}/{offset}/{amount}";
            public const string VideoInfo = Prefix + "video-info/{name}";
            public const string LoadMoreReacts = Prefix + "load-more-reacts/{VideoInfoId}/{Offset}/{Size}";
            public const string LoadMoreComments = Prefix + "load-more-comments/{VideoInfoId}/{Offset}/{Size}";
            public const string LoadMoreCommentReacts = Prefix + "load-more-comment-reacts/{VideoInfoId}/{Offset}/{Size}";
            public const string AddReact = Prefix + "add-react";
            public const string RemoveReact = Prefix + "remove-react";
            public const string AddComment = Prefix + "add-comment";
            public const string RemoveComment = Prefix + "remove-comment";
            public const string AddCommentReact = Prefix + "add-comment-react";
            public const string RemoveCommentReact = Prefix + "remove-comment-react";
            public const string Verses = Prefix + "verses";

        }




        public static class Home
        {
            private const string Prefix = Root + "home/";
            public const string NewFeeds = Prefix + "new-feeds";
            public const string AddPost = Prefix + "add-post";
            public const string FindUsersByName = Prefix + "find-users-by-name/{name}";
            public const string SearchPosts = Prefix + "search-posts/{keyword}";
       

        }

        public static class Notification
        {
            private const string Prefix = Root + "notification/";
            public const string GetNotificationById = Prefix + "get-notification-by-id/{notificationId}";
            public const string Recent = Prefix + "recent";
            public const string LoadMoreNotifications = Prefix + "load-more-notifications/{Offset}/{Size}";
            public const string Seen = Prefix + "seen/{NotificationId}";
            public const string Delete = Prefix + "delete";
        }
        public static class Post
        {
            private const string Prefix = Root + "post/";
            public const string GetPostById = Prefix + "get-post-by-id/{PostId}";
            public const string GetPostByIdForComment = Prefix + "get-post-by-id-for-comment/{PostId}/{CommentId}";
            public const string LoadMorePostReacts = Prefix + "load-more-post-reacts/{PostId}/{Offset}/{Size}";
            public const string LoadMoreComments = Prefix + "load-more-comments/{PostId}/{Offset}/{Size}";
            public const string LoadMoreCommentReacts = Prefix + "load-more-comment-reacts/{PostId}/{Offset}/{Size}";
            public const string LoadMoreShares = Prefix + "load-more-shares/{PostId}/{Offset}/{Size}";
            public const string AddPostReact = Prefix + "add-post-react";
            public const string RemovePostReact = Prefix + "remove-post-react";
            public const string AddComment = Prefix + "add-comment";
            public const string RemoveComment = Prefix + "remove-comment";
            public const string AddCommentReact = Prefix + "add-comment-react";
            public const string RemoveCommentReact = Prefix + "remove-comment-react";
            public const string SharePost = Prefix + "share-post";
            public const string EditPost = Prefix + "edit-post";
            public const string DeletePost = Prefix + "delete-post";
            public const string Verses = Prefix + "verses";

        }



        public static class Profile
        {
            private const string Prefix = Root + "profile/";
            public const string UserPosts = Prefix + "user-posts/{UserId}";
            public const string UserFollowers = Prefix + "user-followers/{UserId}";
            public const string UserFollowings = Prefix + "user-followings/{UserId}";
            public const string SearchUserFollowers = Prefix + "search-user-followers/{UserId}/{KeyWord}";
            public const string SearchUserFollowings = Prefix + "search-user-followings/{UserId}/{KeyWord}";
            public const string UserProfile = Prefix + "user-profile/{UserId}";
            public const string EditCoverPicture = Prefix + "edit-cover-picture";
            public const string EditProfilePicture = Prefix + "edit-profile-picture";
            public const string CheckFollowing = Prefix + "check-following/{UserId}";
            public const string FollowUser = Prefix + "follow-user";
            public const string UnfollowUser = Prefix + "unfollow-user";
            public const string AboutInfo = Prefix + "about-info/{UserId}";

        }

        public static class Quran
        {

            private const string Prefix = Root + "quran/";
            public const string QuranInfo = Prefix + "quran-info/{type}";
            public const string MindMap = Prefix + "mindMap/{id}";
            public const string Note = Prefix + "note/{index}";
            public const string CreateNote = Prefix + "create-note";
          

        }

        public static class Hadith
        {

            private const string Prefix = Root + "hadith/";
            public const string GetAll = Prefix + "get-all";
            public const string GetSectionById = Prefix + "section/{id}";
            public const string GetHadithById = Prefix + "hadith/{id}";
        }

        public static class Session
        {
            private const string Prefix = Root + "session/";
            public const string State = Prefix + "state";
        }
    }
}
