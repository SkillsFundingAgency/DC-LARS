<template>
    <div class="filter-feedback-container" style="display: block;">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <a href="#" class="filter-feedback" v-for="filter in filters"> 
                    <span class="filter-name"> 
                        <span class="close"></span>
                        {{ filter }}
                    </span> 
                </a>
            </span>
        </span>
    </div>
</template>

<script>
    export default {
        el: '#filter-feedback',
        props: {
            filters: {
                type : Object
            }
        }
    }
</script>