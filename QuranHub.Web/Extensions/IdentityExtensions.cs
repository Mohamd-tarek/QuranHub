
namespace QuranHub.Web.Extensions;
    public static class IdentityExtensions {
        public static bool Process(this IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (IdentityError err in result.Errors ?? Enumerable.Empty<IdentityError>()) 
            {
                modelState.AddModelError(string.Empty, err.Description);
            }
            
            return result.Succeeded;
        }

        public static string ConcatError(this ModelStateDictionary modelState)
        {
            string errors = "";

            foreach (var modelStateValue in modelState.Values) {
                foreach (ModelError error in modelStateValue.Errors) {
                    errors += error.ErrorMessage + "\n";
                }
            } 

            return errors;
        }
    }

