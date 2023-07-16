
namespace QuranHub.DAL.Database;

public class IdentitySeedData
{
    public static async Task  SeedDatabaseAsync(IServiceProvider provider)
    {
        provider.GetRequiredService<IdentityDataContext>().Database.Migrate();

        IdentityDataContext IdentityContext = provider.GetRequiredService<IdentityDataContext>();

        if (IdentityContext.Posts.Count() == 0 )
        {
            UserManager<QuranHubUser> userManager = provider.GetRequiredService<UserManager<QuranHubUser>>();

            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            QuranContext QuranContext = provider.GetRequiredService<QuranContext>();

            var userSeedData = new List<(string, string, string, string)>
            {
                ("mohamed_tarek" ,"mohamed@example.com", "Mohamed123$", "User" ),
                ("mazen_tarek","mazen@example.com", "Mazen123$", "User" ),
                ("menna_tarek", "menna@example.com", "Menna123$", "User" )
            };

            foreach (var user in userSeedData)
            {
                await SeedUserAsync(userManager, roleManager, user.Item1, user.Item2, user.Item3, user.Item4);
            }

            await SeedPostsAsync(userManager, IdentityContext, QuranContext);

            await SeedFollowsAsync(userManager, IdentityContext);
        }
    } 

    public static async Task SeedUserAsync(UserManager<QuranHubUser> userManager, RoleManager<IdentityRole> roleManager, string _username, string _email, string _password, string _userRole)
    {
        IdentityRole role = await roleManager.FindByNameAsync(_userRole);

        QuranHubUser user = await userManager.FindByNameAsync(_username);

        if (role == null)
        {
            role = new IdentityRole(_userRole);

            IdentityResult result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception("Cannot create role : " + result.Errors.FirstOrDefault());
            }
        }

        if (user == null)
        {
            user = new QuranHubUser(_username, _email);

            user.EmailConfirmed = true;

            IdentityResult result = await userManager.CreateAsync(user, _password);

            if (!result.Succeeded)
            {
                throw new Exception("Cannot create user : " + result.Errors.FirstOrDefault());
            }
        }

        if (!await userManager.IsInRoleAsync(user, _userRole))
        {
            IdentityResult result = await userManager.AddToRoleAsync(user, _userRole);

            if (!result.Succeeded)
            {
                throw new Exception("Cannot add user to role : " + result.Errors.FirstOrDefault());
            }
        }
    }

    public static async Task  SeedPostsAsync(UserManager<QuranHubUser> userManager, IdentityDataContext IdentityContext, QuranContext QuranContext) 
    {
        await IdentityContext.Verses.AddRangeAsync(QuranContext.Quran.Select(q =>new Verse {VerseId = q.Id,  Index = q.Index, Sura = q.Sura, Aya = q.Aya, Text = q.Text}));

        await IdentityContext.SaveChangesAsync();

        IEnumerable<QuranHubUser> users = await userManager.Users.ToListAsync();

        foreach (var user in  users)
        {
            IEnumerable<ShareablePost> posts = new List<ShareablePost> 
            {
                new ShareablePost
                {
                    DateTime = DateTime.Now,
                    QuranHubUserId = user.Id,
                    VerseId = 1,
                    Text = "first post"
                },
                new ShareablePost
                {
                    DateTime = DateTime.Now,
                    QuranHubUserId = user.Id,
                    VerseId = 2,
                    Text = "second post"
                },
                new ShareablePost
                {
                    DateTime = DateTime.Now,
                    QuranHubUserId = user.Id,
                    VerseId = 3,
                    Text = "third post"
                },
            };

            await IdentityContext.ShareablePosts.AddRangeAsync(posts);

            await IdentityContext.SaveChangesAsync();
        }
    } 

    public static async Task SeedFollowsAsync(UserManager<QuranHubUser> userManager, IdentityDataContext IdentityContext)
    {
        var  FollowRelations = new List<(string, string)>
        {
            ("mazen@example.com" , "mohamed@example.com"),
            ("mohamed@example.com", "mazen@example.com" ),
            ("mazen@example.com", "menna@example.com" ),
            ("menna@example.com", "mazen@example.com" ),
            ("menna@example.com", "mohamed@example.com" ),
            ("mohamed@example.com", "menna@example.com" ),
        };

        IEnumerable<QuranHubUser> users = userManager.Users;

        foreach (var follow in  FollowRelations)
        {
            QuranHubUser follower = await userManager.FindByEmailAsync(follow.Item1);

            QuranHubUser followed = await userManager.FindByEmailAsync(follow.Item2);

            Follow followRelation = new Follow 
            {
                DateTime = DateTime.Now,
                FollowerId = follower.Id,
                FollowedId = followed.Id
            };
            
            await IdentityContext.Follows.AddAsync(followRelation);
        }

        await IdentityContext.SaveChangesAsync();
    }        
}
