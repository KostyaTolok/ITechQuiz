import {Option} from "./option";

export class Question{
    public constructor(
        public id?:string,
        public title?:string,
        public multiple?:boolean,
        public maxSelected?:number,
        public required?:boolean,
        public surveyId?:string,
        public options?: Option[]
    )
    { }
}