import { FC } from 'react'
import LoginInput from './UI/imput/LoginInput'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'

const LoginForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <p style={{display:'flex'}}>Введите свой email</p>
                <LoginInput></LoginInput>
                <LoginButton variant={ButtonVariant.primary} text='Получить код'></LoginButton>
                
        </div>

    )
}

export default LoginForm