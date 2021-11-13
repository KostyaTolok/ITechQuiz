export class AssignRequest{
    constructor(
        public userName?: string,
        public roleName?: string,
        public id?: string,
        public isRejected?: boolean,
        public createdDate?: string
    ) {
    }
}