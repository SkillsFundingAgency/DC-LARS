﻿using ESFA.DC.LARS.API.Models;
using Microsoft.Azure.Search.Models;

namespace ESFA.DC.LARS.API.Interfaces.Services
{
    public interface IODataQueryService
    {
        void SetLevelFilters(SearchModel searchModel, SearchParameters parameters);
    }
}