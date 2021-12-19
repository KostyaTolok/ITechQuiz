export class OptionStatistics{
    public constructor(
        public optionTitle?:string,
        public isCorrect?:boolean,
        public answersAmount?: number,
        public answersPercent?: number
    )
    { }
}