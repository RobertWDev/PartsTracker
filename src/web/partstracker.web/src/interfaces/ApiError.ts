export interface ApiError {
    type: string
    title: string
    status: number
    detail: string
    errors: Error[]
}

export interface Error {
    code: string
    description: string
    type: number
}