export enum FilterType {
    AwardingBody = "AwardingBody",
    Levels = "Levels",
    FundingStreams = "FundingStreams",
    TeachingYears = "TeachingYears"
}

export interface IFilterItem {
    key : string;
    value : string;
    type : FilterType;
}