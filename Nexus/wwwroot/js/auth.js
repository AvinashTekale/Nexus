// Initialize the loader
$(document).ready(function () {
    // Create a loader element dynamically
    const loader = $('<div id="ajaxLoader" class="loader-overlay d-none"><div class="spinner-border text-primary" role="status"></div></div>');
    $('body').append(loader);

    // Show loader on AJAX start and hide it on AJAX stop
    $(document).ajaxStart(function () {
        $('#ajaxLoader').removeClass('d-none'); // Show loader
    }).ajaxStop(function () {
        $('#ajaxLoader').addClass('d-none'); // Hide loader
    });
});

// Function to handle form submission via AJAX for login and register
function handleFormSubmission(formId, url, successMessage, errorMessage) {
    $(formId).submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        // Clear previous error messages
        clearErrors();

        // Get the form data
        var formData = $(this).serialize();

        // Send AJAX request
        $.ajax({
            url: url,  // URL passed dynamically
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    // Show success message
                    $('#message').text(successMessage);

                    // Clear the form after successful submission
                    $(formId)[0].reset();

                    // Optionally redirect or update the URL
                    updateUrl(response.redirectUrl);

                    handlePageNavigation(null, response.redirectUrl);
                } else {
                    // Show error messages
                    displayErrors(response.errors);
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                alert(errorMessage);
            }
        });
    });
}

// Function to clear all error messages
function clearErrors() {
    $('.text-danger').text('');
    $('#message').text('');
}

// Function to display error messages for each field
function displayErrors(errors) {
    if (errors) {
        if (errors.Name) {
            $('#NameError').text(errors.Name);
        }
        if (errors.Email) {
            $('#EmailError').text(errors.Email);
        }
        if (errors.Password) {
            $('#PasswordError').text(errors.Password);
        }
    }
}

// Function to update the URL without reloading the page
function updateUrl(url) {
    history.pushState(null, '', url);
}

// Reusable function to handle AJAX navigation
function handlePageNavigation(linkId, targetUrl) {
    if (linkId) {
        $(linkId).click(function (event) {
            event.preventDefault();
            navigateToPage(targetUrl); // Call the reusable navigation function
        });
    } else {
        navigateToPage(targetUrl); // Allow navigation without click event
    }
}

// Core function to handle AJAX page navigation
function navigateToPage(targetUrl) {
    $.ajax({
        url: targetUrl,
        type: 'GET',
        success: function (response) {
            $('body').html(response); // Replace the page content with the target page
            updateUrl(targetUrl); // Update the URL without reloading
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert('An error occurred while loading the page.');
        }
    });
}
function handleLogout() {
    $.ajax({
        url: '/Auth/Logout',
        type: 'GET',
        success: function () {
            // Redirect to the login page after logout
            window.location.href = '/Auth/Login';
        },
        error: function () {
            alert("An error occurred during logout.");
        }
    });
}
(function () {
    const loaderCSS = `
        .loader-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1050;
            background-color: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .loader-overlay .spinner-border {
            width: 3rem;
            height: 3rem;
        }
    `;

    // Check if the style already exists
    if (!document.getElementById('loaderStyle')) {
        const styleTag = document.createElement('style');
        styleTag.id = 'loaderStyle';
        styleTag.type = 'text/css';
        styleTag.appendChild(document.createTextNode(loaderCSS));
        document.head.appendChild(styleTag);
    }

    // Function to show loader
    window.showLoader = function () {
        if (!document.getElementById('loaderOverlay')) {
            const loaderOverlay = document.createElement('div');
            loaderOverlay.id = 'loaderOverlay';
            loaderOverlay.className = 'loader-overlay';

            const spinner = document.createElement('div');
            spinner.className = 'spinner-border text-light';
            spinner.setAttribute('role', 'status');

            loaderOverlay.appendChild(spinner);
            document.body.appendChild(loaderOverlay);
        }
    };

    // Function to hide loader
    window.hideLoader = function () {
        const loaderOverlay = document.getElementById('loaderOverlay');
        if (loaderOverlay) {
            loaderOverlay.remove();
        }
    };
})();



//Left menu
function toggleMenu() {
    const menu = document.getElementById('leftMenu');
    menu.classList.toggle('collapsed');
}