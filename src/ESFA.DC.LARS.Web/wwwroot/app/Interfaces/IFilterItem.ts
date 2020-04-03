export enum FilterType {
    AwardingBody,
    Levels,
    FundingStreams,
    TeachingYears,
    FrameworkTypes,
    IssuingAuthorities
}

export interface IFilterItem {
    key : string;
    value : string;
    type : FilterType;
}