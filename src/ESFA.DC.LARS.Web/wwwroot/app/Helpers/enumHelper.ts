import { SearchType } from "../SearchType";

export class EnumHelper {
    public ConvertServerEnumValueToClientEnum(value: string): SearchType {
        if (value === "1") {
            return SearchType.Frameworks;
        }
        if (value === "2") {
            return SearchType.Units;
        }
        return SearchType.Qualifications;
    }
}
export const enumHelper = new EnumHelper();