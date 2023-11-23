﻿using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Interfaces.Services;

public interface IDisposablePromptService
{
    /// <summary>
    /// Get prompt's user-creator id
    /// </summary>
    /// <param name="disposablePromptId">Id of disposable prompt</param>
    /// <returns>Result with user's id, or error</returns>
    Task<Result<long>> GetUserIdByPrompt(long disposablePromptId);
    
    /// <summary>
    /// Get all prompts by user id
    /// </summary>
    /// <param name="userId">User-creator id</param>
    /// <returns>Result with collection of disposable prompts bound to user with given id</returns>
    Task<Result<IQueryable<DisposablePrompt>>> GetAllByUserId(long userId);
    
    /// <summary>
    /// Create new disposable prompt
    /// </summary>
    /// <param name="userId">Id of user-creator</param>
    /// <param name="title">Title of prompt</param>
    /// <param name="description">Extra description of prompt</param>
    /// <param name="showsAt">Show time for client side</param>
    /// <returns>Result with disposable prompt, or error</returns>
    Task<Result<DisposablePrompt>> Create(long userId, string title, string? description, DateTime showsAt);
    
    /// <summary>
    /// Delete disposable prompt by prompt id
    /// </summary>
    /// <param name="disposablePromptId">Id of disposable prompt to delete</param>
    /// <returns>Empty result, or with error</returns>
    Task<Result<None>> DeleteById(long disposablePromptId);
}