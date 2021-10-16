import {Question} from "./question";

export class Survey{
    public constructor(
        public id?:string,
        public name?:string,
        public title?:string,
        public type?:string,
        public subtitle?:string,
        public createdDate?:string,
        public questions?: Question[]
    ) 
    { }
}