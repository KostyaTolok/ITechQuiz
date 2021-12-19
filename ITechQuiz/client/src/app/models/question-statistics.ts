import {OptionStatistics} from "./option-statistics";

export class QuestionStatistics {
    public constructor(
        public questionTitle?: string,
        public required?: boolean,
        public optionsStatistics: OptionStatistics[] = []
    ) {
    }
}