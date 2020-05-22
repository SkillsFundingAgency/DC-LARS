import { SearchType } from "../Enums/SearchType";

export class EnumHelper {
    public ConvertServerEnumValueToClientEnum(value: string): SearchType {
        if (value === "1") {
            return SearchType.Frameworks;
        }
        if (value === "2") {
            return SearchType.Units;
        }
        if (value === "3") {
            return SearchType.Standards; 
        }
        return SearchType.Qualifications;
    }
}
export const enumHelper = new EnumHelper();