using LessonFour.Dto;

namespace GraphQLApi.GraphQLs
{
    public class Mutation
    {
        private readonly IGraphService _service;
        public Mutation(IGraphService service)
        {
            _service = service;
        }


        public async Task<string> Login(UserAuthorizationRequest request)
            => await _service.Post("https://localhost:7224/registration/login", request);

        public async Task<string> Register(UserAuthorizationRequest request)
        => await _service.Post("https://localhost:7224/registration/register", request);
    }
}

