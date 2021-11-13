export class ChangePasswordModel{
    public constructor(
        public email?:string,
        public oldPassword?:string,
        public newPassword?:string
    )
    { }
}