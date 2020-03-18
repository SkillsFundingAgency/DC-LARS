export enum FilterType {
    AwardingBody,
    Levels,
    FundingStreams,
    TeachingYears
}

export interface IFilterItem {
    key : string;
    value : string;
    type : FilterType;
}