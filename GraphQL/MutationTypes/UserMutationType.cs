using AutoMapper;
using HotChocolate;
using Magicord.Models;
using Magicord.Modules.Users;

namespace Magicord.GraphQL.MutationTypes
{
  public partial class Mutation
  {
    public User CreateUser([Service] IUserService userService, CreateUserInputDto input)
    {
      return userService.CreateUser(input);
    }
  }
}