import {Option} from "./option";
import {Question} from "./question";

export class Answer {
    public constructor(
        public id?: string,
        public questionId?: string,
        public questionTitle?: string,
        public questionRequired?: boolean,
        public isAnonymous?: boolean,
        public selectedOptions: Option[] = [],
        public isAnonymousAllowed?: boolean,
        public createdDate?: string
    ) {
    }
}