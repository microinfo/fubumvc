/** @jsx React.DOM */

var React = require('react');

var rb = require('react-bootstrap');
var Navbar = rb.Navbar;
var Nav = rb.Nav;
var DropdownButton = rb.DropdownButton;
var MenuItem = rb.MenuItem;
var NavItem = rb.NavItem;
var Router = require('react-router');


var Header = React.createClass({
	mixins: [ Router.State ],

	render: function(){
		var path = this.getPathname();
		var sectionLinks = [];
		if (path != '/'){
			var sectionKey = path.split('/')[1];
			var activeSection = FubuDiagnostics.section(sectionKey);
			sectionLinks = activeSection.activeViews().map(view => {
				return (
					<NavItem href={view.anchor} title={view.description}>{view.title}</NavItem>
				);
			});
		}

		var sectionItems = FubuDiagnostics.sections.map(section => {
			return (<NavItem href={section.anchor} title={section.description}>{section.title}</NavItem>);
		});

		return (
		
			<div>
				<Navbar inverse={true} id="top-nav">
					<Nav>
						<a className="navbar-brand" href="#/">FubuMVC Diagnostics</a>

				        <NavItem eventKey="4" href="#/">Application Name Here?</NavItem>
						{sectionLinks}
					</Nav>
					<Nav right={true}>
					    <DropdownButton eventKey={1} title="Sections">
				          {sectionItems}
				        </DropdownButton>
					</Nav>
				</Navbar>

			</div>
		);
	}
});

module.exports = Header;