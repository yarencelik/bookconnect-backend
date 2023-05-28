using App.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Common;

public abstract class BaseController : ControllerBase
{
    protected IMediator mediator;
    protected BaseController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}