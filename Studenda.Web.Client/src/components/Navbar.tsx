import { FC } from 'react'

const Navbar: FC = () => {

    return (
        <div style={{display:'flex',border:'2px solid lightgray',padding:'5px'}}>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/email'>email</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/f1'>f1</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/login'>login</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/groupselector'>group selector</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/schedule'>Schedule</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/journal'>Journal</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/homework'>Homework</a>
               <a style={{margin:'10px', border:'2px solid lightgray',padding:'5px'}} href='/attendance'>Attendence</a>
        </div>

    )
}

export default Navbar