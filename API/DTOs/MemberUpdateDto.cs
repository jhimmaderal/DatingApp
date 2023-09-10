namespace API.DTOs
{
    public class MemberUpdateDto
    {
        // AUTOMAP CREATED FOR CREATING QUERY
        // USER CONTROLLER HTTPPUT FOR UDPATING
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}