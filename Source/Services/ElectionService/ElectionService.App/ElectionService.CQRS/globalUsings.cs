global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;




global using ElectionService.Shared.Enums;
global using ElectionService.Database;
global using ElectionService.Entities;
global using ElectionService.CQRS.Common.Exceptions;
global using ElectionService.CQRS.Extensions;
global using ElectionService.CQRS.Common.Interfaces;
global using ElectionService.CQRS.Common.Implementations;
global using ElectionService.CQRS.Common.Enums;
global using ElectionService.CQRS.Common.Base;
global using ElectionService.CQRS.Features.Cache;
global using ElectionService.CQRS.MessageConsumers;



global using AutoMapper;
global using MediatR;
global using MassTransit;


global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http.Headers;
global using Microsoft.EntityFrameworkCore;



global using RabbitMq.Settings;
global using RabbitMq.Settings.QueueRoutes;
global using RabbitMq.Messages;
global using RabbitMq.Messages.AuthServiceMessages;


