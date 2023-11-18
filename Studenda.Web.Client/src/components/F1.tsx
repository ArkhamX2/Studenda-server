import { FC } from 'react'
import LoginButton, { ButtonVariant } from './UI/button/LoginButton'


const F1: FC = () => {

    return (
        <main>
        <div className='LoginBox'>
                <div className="Logo" style={{margin:'20px'}}></div>
                <LoginButton variant={ButtonVariant.primary} text='Войти'></LoginButton>
                <LoginButton variant={ButtonVariant.outlined} text='Войти как гость'></LoginButton>
	</div>
        </main>
    )
}

export default F1