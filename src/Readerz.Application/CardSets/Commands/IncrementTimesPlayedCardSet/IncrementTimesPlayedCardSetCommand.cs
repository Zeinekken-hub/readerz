﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readerz.Application.Common.Exceptions;
using Readerz.Application.Common.Interfaces;
using Readerz.Domain.Entities;

namespace Readerz.Application.CardSets.Commands.IncrementTimesPlayedCardSet
{
    public class IncrementTimesPlayedCardSetCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class IncrementTimesPlayedCardSetCommandHandler : IRequestHandler<IncrementTimesPlayedCardSetCommand>
    {
        private readonly IApplicationDbContext _context;

        public IncrementTimesPlayedCardSetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(IncrementTimesPlayedCardSetCommand request, CancellationToken cancellationToken)
        {
            var cardSet = await _context.CardSets.FirstOrDefaultAsync(entity => entity.Id == request.Id, 
                cancellationToken);
            
            if (cardSet == null)
            {
                throw new NotFoundException(nameof(request.Id), typeof(CardSet));
            }

            cardSet.TimesPlayed++;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
    
}