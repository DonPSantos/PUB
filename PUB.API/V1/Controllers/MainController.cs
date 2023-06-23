﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using PUB.Domain.Interfaces;
using PUB.Domain.Notifications;
using AutoMapper;

namespace PUB.API.V1.Controllers
{
    public class MainController : ControllerBase
    {
        protected readonly INotificator _notificator;
        protected readonly IMapper _mapper;

        protected MainController(INotificator notificator, IMapper mapper)
        {
            _notificator = notificator;
            _mapper = mapper;
        }

        protected bool IsOperationValid()
        {
            return !_notificator.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
                return Ok(new
                {
                    sucesso = true,
                    data = result
                });

            return Ok(new
            {
                sucesso = false,
                erros = _notificator.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificator.Handle(new Notification(mensagem));
        }
    }
}