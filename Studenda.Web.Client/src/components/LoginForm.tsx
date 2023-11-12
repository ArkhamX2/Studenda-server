import { FC } from 'react'
import LoginInput from './UI/imput/LoginInput'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'
import LoginLabel, { TextAlign } from './UI/label/LoginLabel'

const LoginForm: FC = () => {

    return (
        <div style={{position:'absolute', left:'40%', top:'30%', display:'flex',flexDirection:'column',border:'2px solid lightgray',padding:'50px'}}>
                <LoginLabel text='Введите свой email'></LoginLabel>
                <LoginInput></LoginInput>
                <LoginButton variant={ButtonVariant.primary} text='Получить код'></LoginButton>
                
        </div>

    )
}

export default LoginForm