export class Option{
    public constructor(
        public id?:string,
        public title?:string,
        public isCorrect?:boolean,
        public subtitle?:string,
        public questionId?:string
    )
    { }
}