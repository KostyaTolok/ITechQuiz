import {Survey} from "./survey";
import {AssignRequest} from "./assign-request";

export class User {
    constructor(
        public id?: string,
        public userName?: string,
        public email?: string,
        public lockoutEnd?: Date,
        public lockoutEnabled?: boolean,
        public accessFailedCount?: 0,
        public surveys: Survey[] = [],
        public assignRequests: AssignRequest[] = [],
        public isDisabled?: boolean,
        public disabledEnd?: Date,
        public roles: string[] = []
    ) {
    }
}