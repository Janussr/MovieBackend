using AutoMapper;
using CodeFirstProject.DbConnector;
using CodeFirstProject.DTOs;
using CodeFirstProject.Models;

namespace CodeFirstProject.Services
{
    public class UserService
    {
        // Instance variables
        private readonly UserContext _context;
        private readonly IMapper _mapper;


        //Constructor
        public UserService(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Get all Users
        public List<UserDTO> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var userDTO = _mapper.Map<List<UserDTO>>(users);
            return userDTO;
        }


        public UserDTO GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) {
                throw new InvalidOperationException("User not found");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }


        public UserDTO AddUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO); // Map DTO to entity

            _context.Users.Add(user);
            _context.SaveChanges();

            var addedUserDto = _mapper.Map<UserDTO>(user); // Map entity back to DTO
            return addedUserDto;
        }



        public UserDTO UpdateUser(int userId, UserDTO model)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            // Map properties from DTO to entity
            _mapper.Map(model, user);

            _context.SaveChanges();

            // Map updated entity back to DTO
            var updatedUserDto = _mapper.Map<UserDTO>(user);
            return updatedUserDto;
        }


        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
      

    }

}
