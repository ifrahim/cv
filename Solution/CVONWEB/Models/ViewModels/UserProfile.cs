using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVOnWeb.Models.ViewModels
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public int UserProfileViews { get; set; }
        public DateTime UserCreationDate { get; set; }
        public int UserProfileNode { get; set; }
        public string UserProfilePicture { get; set; }

        public string UserAddressLine1 { get; set; }
        public string UserAddressLine2 { get; set; }
        public string UserCity { get; set; }
        public string UserCounty { get; set; }
        public string UserPostcode { get; set; }
        public string UserCounrty { get; set; }
        public bool ShowAddress { get; set; }

        public string UserTelephone { get; set; }
        public bool ShowTelephone { get; set; }

        public IList<Link> UserLinks { get; set; }
        public bool ShowLinks { get; set; }

        public string UserCVTitle { get; set; }
        public string UserProfileSummary { get; set; }
        public string UserPreferredJobType { get; set; }
        public string UserLocationPreferences { get; set; }
        public string UserGender { get; set; }
        public string UserNationality { get; set; }
        public bool ShowPreferredJobType { get; set; }
        public bool ShowLocationPreferences { get; set; }
        public bool ShowGender { get; set; }
        public bool ShowNationality { get; set; }
        
        public IList<Skill> UserSkills { get; set; }
        public bool ShowSkills { get; set; }

        public IList<Experience> UserExperience { get; set; }
        public bool ShowExperience { get; set; }

        public IList<Education> UserEducation { get; set; }
        public bool ShowEducation { get; set; }

        public IList<Certifications> UserCertification { get; set; }
        public bool ShowCertification { get; set; }

        public IList<Recommendation> UserRecommendations { get; set; }
        public bool ShowRecommendations { get; set; }

        public  IList<Portfolio> UserPortfolio { get; set; }
        public bool ShowPortfolio { get; set; }

        public IList<AddtionalInfo> UserAdditionalInformation { get; set; }
    }
}