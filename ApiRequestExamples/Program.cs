using System;
using TheBattleShipClient.Services;
using static TheBattleShipClient.Services.RoomsService;

namespace ApiRequestExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("User-1 token:");
            var loginResult1 = IdentityService.Login(new UserRequest
            {
                UserName = "user",
                Email = "user123@gmail.com",
                Password = "Password123!"
            });
            var token1 = loginResult1.Result.Token;
            Console.WriteLine(token1);

            var createRoomResult = RoomsService.CreateRoom(token1, new RoomRequest { Size = 10 });
            Console.WriteLine("Room ID (Create):");
            var roomId = createRoomResult.Result.Id;
            Console.WriteLine(roomId);
            
            Console.WriteLine("User-2 token:");
            var loginResult2 = IdentityService.Login(new UserRequest
            {
                UserName = "user",
                Email = "user12345@gmail.com",
                Password = "Password123!"
            });
            var token2 = loginResult2.Result.Token;
            Console.WriteLine(token2);

            var joinRoomResult = RoomsService.JoinRoom(token2, roomId);
            Console.WriteLine("Room ID (Join):");
            Console.WriteLine(joinRoomResult.Result.Id);

        }
    }
}
