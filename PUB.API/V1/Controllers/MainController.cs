using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using PUB.Domain.Interfaces;
using PUB.Domain.Notifications;
using AutoMapper;
using System.Net;
using PUB.API.Customizations;
using PUB.API.Extensions;

namespace PUB.API.V1.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected readonly INotificator _notificator;
        protected readonly IMapper _mapper;
        protected readonly ILogger<MainController> _logger;

        protected MainController(ILogger<MainController> logger, INotificator notificator, IMapper mapper)
        {
            _notificator = notificator;
            _mapper = mapper;
            _logger = logger;
        }

        #region ActionResults

        protected IActionResult ResponseOk(object result) => Response(HttpStatusCode.OK, result);

        protected IActionResult ResponseOk() => Response(HttpStatusCode.OK);

        protected IActionResult ResponseCreated(object data) => Response(HttpStatusCode.Created, data);

        protected IActionResult ResponseNoContent() => Response(HttpStatusCode.NoContent);

        protected IActionResult ResponseNotModified() => Response(HttpStatusCode.NotModified);

        protected IActionResult ResponseConflict() => Response(HttpStatusCode.Conflict, errorMessage: "Registro já existe no banco de dados");

        protected IActionResult ResponseBadRequest(string errorMessage) => Response(HttpStatusCode.BadRequest, errorMessage: errorMessage);

        protected IActionResult ResponseBadRequest() => Response(HttpStatusCode.BadRequest, errorMessage: "Requisição invalida");

        protected IActionResult ResponseBadRequest(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }

            return Response(HttpStatusCode.BadRequest);
        }

        protected IActionResult ResponseNotFound(string errorMessage) => Response(HttpStatusCode.NotFound, errorMessage: errorMessage);

        protected IActionResult ResponseNotFound() => Response(HttpStatusCode.NotFound, errorMessage: "Recurso não encontrado");

        protected IActionResult ResponseUnauthorized(string errorMessage) => Response(HttpStatusCode.Unauthorized, errorMessage: errorMessage);

        protected IActionResult ResponseUnauthorized() => Response(HttpStatusCode.Unauthorized, errorMessage: "Permissão negada");

        protected IActionResult ResponseInternalServerError() => Response(HttpStatusCode.InternalServerError);

        protected IActionResult ResponseInternalServerError(string errorMessage) => Response(HttpStatusCode.InternalServerError, errorMessage: errorMessage);

        protected IActionResult ResponseInternalServerError(Exception exception) => Response(HttpStatusCode.InternalServerError, errorMessage: exception.Message);

        #endregion ActionResults

        protected void NotificarErro(string mensagem)
        {
            _notificator.Handle(mensagem);
        }

        protected bool IsOperationValid()
        {
            return !_notificator.HasNotification();
        }

        #region JsonResult Build

        private new JsonResult Response(HttpStatusCode statusCode, object data, string errorMessage)
        {
            CustomResult result = null;
            if (string.IsNullOrWhiteSpace(errorMessage) && !_notificator.HasNotification())
            {
                var success = statusCode.IsSuccess();

                if (data is not null)
                    result = new CustomResult(statusCode, success, data);
                else
                    result = new CustomResult(statusCode, success);
            }
            else
            {
                var errors = new List<string>();

                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);

                if (_notificator.HasNotification())
                    errors.AddRange(_notificator.GetNotifications());

                result = new CustomResult(statusCode, false, errors);
            }

            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }

        private new JsonResult Response(HttpStatusCode statusCode, object data) => Response(statusCode, data, null);

        private new JsonResult Response(HttpStatusCode statusCode, string errorMessage) => Response(statusCode, null, errorMessage);

        private new JsonResult Response(HttpStatusCode statusCode) => Response(statusCode, null, null);

        #endregion JsonResult Build
    }
}