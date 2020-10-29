﻿using LRSIntroductoryWebApi.Data;
using LRSIntroductoryWebApi.DTO;
using LRSIntroductoryWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntroductoryWebApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>A list of users</returns>
        public async Task<ActionResult<IEnumerable<User>>> GetUserList()
        {
            return await _userRepository.GetUsers();
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user which has the specified id</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">userId</exception>
        public async Task<ActionResult<User>> FetchUserByID(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            return await _userRepository.GetUserByID(userId);
        }

        /// <summary>
        /// Updates the user's data with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The updated user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">id</exception>
        /// <exception cref="System.ArgumentNullException">user</exception>
        /// <exception cref="System.NullReferenceException">user modified</exception>
        public async Task<ActionResult<User>> EditUser(int id, UserDTO userDto)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (userDto is null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            return await _userRepository.UpdateUser(id, userDto);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The created user</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await _userRepository.InsertUser(user);
        }


        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The deleted user</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">id</exception>
        public async Task<ActionResult<User>> DisableUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return await _userRepository.DeleteUser(id);
        }
    }
}
