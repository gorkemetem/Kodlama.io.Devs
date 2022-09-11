using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Service.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<CreatedLanguageDto>
    {
        public string Name { get; set; }

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreatedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<CreatedLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageNameCanNotBeDuplicateWhenInserted(request.Name);

                Language mapperLanguage = _mapper.Map<Language>(request);
                Language createLanguage = await _languageRepository.AddAsync(mapperLanguage);
                CreatedLanguageDto createdLanguageDto = _mapper.Map<CreatedLanguageDto>(createLanguage);

                return createdLanguageDto;
            }
        }
    }
}
