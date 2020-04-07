export enum FilterType {
    AwardingBodies = "AwardingBodies",
    Levels = "Levels",
    FundingStreams = "FundingStreams",
    TeachingYears = "TeachingYears",
    FrameworkTypes = "FrameworkTypes",
    IssuingAuthorities = "IssuingAuthorities"
}

export interface IFilterItem {
    key : string;
    value : string;
    type : FilterType;
}