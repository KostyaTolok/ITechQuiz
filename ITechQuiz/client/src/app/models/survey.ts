import {Question} from "./question";
import {Category} from "./category";

export class Survey {
    public constructor(
        public id?: string,
        public title?: string,
        public type?: string,
        public subtitle?: string,
        public createdDate?: string,
        public updatedDate?: string,
        public lastPassageDate?: string,
        public questions: Question[] = [],
        public isAnonymousAllowed?: boolean,
        public isMultipleAnswersAllowed?: boolean,
        public categories: Category[] = []
    ) {
    }
}