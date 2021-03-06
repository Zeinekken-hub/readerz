﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Readerz.Application.Common.Interfaces;
using Readerz.Application.Common.Models;

namespace Readerz.Application.Text.Queries.GetWordTranslation
{
    public class GetWordTranslationQuery : IRequest<TranslationResult>
    {
        public string Text { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
    
    public class GetWordTranslationQueryHandler : IRequestHandler<GetWordTranslationQuery, TranslationResult>
    {
        private readonly ITranslationService _translationService;
        public GetWordTranslationQueryHandler(ITranslationService service)
        {
            _translationService = service;
        }
        public async Task<TranslationResult> Handle(GetWordTranslationQuery request, CancellationToken cancellationToken)
        {
            request.From = request.From.ToLower() == "auto" ? string.Empty : request.From;

            return await _translationService.TranslateAsync(request.Text, request.To, request.From);
        }
    }
}
