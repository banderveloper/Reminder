import {INavbar} from "../../interfaces/components/INavbar.ts";
import classes from "./Navbar.module.css";

const Navbar = ({navbarItems}: INavbar) => {

    return (
        <header>

            <nav className={classes.nav}>

                {/* Left side with logo */}
                <div className={`${classes.navGroup} ${classes.navLogoBlock}`}>
                    <span>Logo</span>
                    <span>Reminder</span>
                </div>

                {/* Right side with nav items*/}
                <div className={classes.navGroup}>
                    {
                        navbarItems.map(item => (
                            <a className={classes.navItem} href={item.path} key={item.path}>{item.pathName}</a>
                        ))
                    }
                </div>

            </nav>

        </header>
    );
};

export default Navbar;