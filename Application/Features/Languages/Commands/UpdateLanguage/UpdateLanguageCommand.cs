using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Service.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<UpdatedLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdatedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<UpdatedLanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageNameCanNotBeDuplicateWhenInserted(request.Name);

                Language mapperLanguage = _mapper.Map<Language>(request);
                Language updateLanguage = await _languageRepository.UpdateAsync(mapperLanguage);
                UpdatedLanguageDto updatedLanguageDto = _mapper.Map<UpdatedLanguageDto>(updateLanguage);

                return updatedLanguageDto;
            }
        }
    }
}
